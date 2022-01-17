using System;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Tools.MSBuild;
using Nuke.Common.Tools.NuGet;
using Nuke.Common.Utilities.Collections;
using static Nuke.CodeGeneration.CodeGenerator;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.Tools.MSBuild.MSBuildTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;


[CheckBuildProjectConfigurations]
class Build : NukeBuild
{
    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;
    
    [Parameter("The destination Nuget feed to publish packages to. This defaults to nuget.org.")]
    string NugetApiUrl = "https://api.nuget.org/v3/index.json";
    
    [Parameter("The publishing API key for the package destination feed at the Nuget API URL.")]
    string NugetApiKey;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;
    [GitVersion] readonly GitVersion GitVersion;
    AbsolutePath SourceDirectory => RootDirectory / "source";
    AbsolutePath TestsDirectory => RootDirectory / "tests";
    AbsolutePath OutputDirectory => RootDirectory / "output";
    
    public static int Main () => Execute<Build>(x => x.Compile);

    Target Clean => _ => _
        .Before(Restore)
        .Description("Cleans out the derived files generated from a build.")
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

    Target Compile => _ => _
        .DependsOn(Restore)
        .Description("Compiles the Nuke.Tools.Retype library package from source code.")
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

    Target Package => _ => _
        .DependsOn(Compile)
        .Description("Packages the Nuke.Tools.Retype library for the supported dotnet platforms into a Nuget package file.")
        .Executes(() =>
        {
            DotNetPack(s => s
                .SetVersion(GitVersion.NuGetVersionV2)
                .SetConfiguration(Configuration)
                .SetProject(Solution)
                .SetOutputDirectory(OutputDirectory)
                .EnableNoBuild());
        });
    
    Target Publish => _ => _
        .Description("Publishes the Nuke.build retype tool library to a Nuget package feed.")
        .DependsOn(Package)
        .DependsOn(Clean)
        .Requires(() => NugetApiUrl)
        .Requires(() => NugetApiKey)
        .Requires(() => Configuration.Equals(Configuration.Release))
        .Executes(() =>
        {
            DotNetNuGetPush(s => s
                .SetTargetPath(RootDirectory)
                .SetSource(NugetApiUrl)
                .SetApiKey(NugetApiKey));
        });
    
    Target Generate => _ => _
        .Description("Generates the Retype.Generated.cs code which serves as the basis for the Retype tool's settings and task classes. " +
                     "This manual action only needs to be taken when a new Retype.json version has been defined.")
        .Executes(() =>
        {
            GenerateCode("source/Retype.json", namespaceProvider: tool => $"Nuke.Tools.Retype");
        });
}
