using System.Collections;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.CodeGeneration.CodeGenerator;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Serilog.Log;
using static System.Environment;
// ReSharper disable InconsistentNaming

namespace Build.Torq.Nuke.Retype;

class Builder : NukeBuild
{
    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;
    
    [Parameter("The destination Nuget feed to publish packages to. This defaults to nuget.org.")]
    string NugetApiUrl = "https://api.nuget.org/v3/index.json";
    
    [Parameter("The publishing API key for the package destination feed at the Nuget API URL.")]
    string NugetApiKey;

    [Solution] readonly Solution Solution;
    AbsolutePath SourceDirectory => RootDirectory / "source";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath OutputDirectory => RootDirectory / "output";
    private GitVersion gitVersion;
    
    public static int Main () => Execute<Builder>(x => x.Build);

    Target Clean => _ => _
        .Before(Restore)
        .Description("Deletes the object, executable and package files generated from builds.")
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(DeleteDirectory);
            EnsureCleanDirectory(OutputDirectory);
        });

    Target Restore => _ => _
        .Description("Restores package dependencies from the package source.")
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Build => _ => _
        .DependsOn(Restore)
        .Description("Builds the Torq.Nuke.Retype library from source code.")
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .EnableNoRestore());
        });

    Target Rebuild => _ => _
        .DependsOn(Clean)
        .DependsOn(Build)
        .Description("Cleans out the old generated files then Builds the Torq.Nuke.Retype library from scratch.")
        .Executes(() =>
            Information("Rebuild complete (Cleaned and Built)")
        );

    Target Package => _ => _
        .Description("Packages the Torq.Nuke.Retype library for the supported dotnet platforms into a Nuget package file.")
        .Executes(() =>
        {
            DotNetPack(s => s
                .SetVersion(GitVersion.NuGetVersionV2)
                .SetConfiguration(Configuration)
                .SetProject(Solution)
                .SetOutputDirectory(OutputDirectory)
                .EnableNoRestore()
                .EnableNoBuild());
        });
    
    Target Publish => _ => _
        .Description("Publishes the Nuke.build retype tool library to a Nuget package feed.")
        .DependsOn(Package)
        .Requires(() => NugetApiUrl)
        .Requires(() => NugetApiKey)
        .Requires(() => Configuration.Equals(Configuration.Release))
        .Executes(() =>
        {
            string target = OutputDirectory.GlobFiles("*.nupkg")
                .Single(x => x.Contains(GitVersion.NuGetVersionV2));

            DotNetNuGetPush(s => s
                .SetTargetPath(target)
                .SetSource(NugetApiUrl)
                .SetApiKey(NugetApiKey));
        });
    
    Target Generate => _ => _
        .Description("Generates the Retype.Generated.cs code which serves as the basis for the Retype tool's settings and task classes. " +
                     "This manual action only needs to be taken when a new Retype.json version has been defined.")
        .Executes(() =>
        {
            GenerateCode("source/Retype.json", namespaceProvider: tool => $"Torq.Nuke.Retype");
        });
    
    Target Environment => _ => _
        .Description("Reports on the environment so as to assist in identifying problems associated with the build.")
        .Executes(() =>
        {
            foreach (DictionaryEntry variable in GetEnvironmentVariables())
            {
                Information($"{variable.Key} = {variable.Value}");
            }

            var attribute = new GitVersionAttribute();
            var settings = new GitVersionSettings();
            string toolPath = settings.SetFramework(attribute.Framework).ProcessToolPath;
            string status = File.Exists(toolPath) ? "exists" : "missing";
            Information($"GitVersion tool {status}: {toolPath}");
        });

    private GitVersion GitVersion =>
        gitVersion ??= GitVersionTasks.GitVersion(s => s
                .SetFramework("net5.0")
                .DisableProcessLogOutput()
                .When(Verbosity == Verbosity.Verbose, EnableVerboseLogOutput)
                .EnableNoCache())
            .Result;

    GitVersionSettings EnableVerboseLogOutput(GitVersionSettings settings)
    {
        return settings
            .SetVerbosity(GitVersionVerbosity.debug)
            .EnableProcessLogOutput();
    }
}