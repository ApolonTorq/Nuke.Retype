
using JetBrains.Annotations;
using Newtonsoft.Json;
using Nuke.Common;
using Nuke.Common.Execution;
using Nuke.Common.Tooling;
using Nuke.Common.Tools;
using Nuke.Common.Utilities.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;

namespace Torq.Nuke.Retype
{
    /// <summary>
    ///   <p>Retype is an ultra-high-performance generator that builds a website based on simple text files. Focus on your writing while Retype builds the rest.</p>
    ///   <p>For more details, visit the <a href="https://retype.com">official website</a>.</p>
    /// </summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    public static partial class RetypeTasks
    {
        /// <summary>
        ///   Path to the Retype executable.
        /// </summary>
        public static string RetypePath =>
            ToolPathResolver.TryGetEnvironmentExecutable("RETYPE_EXE") ??
            GetToolPath();
        public static Action<OutputType, string> RetypeLogger { get; set; } = ProcessTasks.DefaultLogger;
        /// <summary>
        ///   <p>Retype is an ultra-high-performance generator that builds a website based on simple text files. Focus on your writing while Retype builds the rest.</p>
        ///   <p>For more details, visit the <a href="https://retype.com">official website</a>.</p>
        /// </summary>
        public static IReadOnlyCollection<Output> Retype(string arguments, string workingDirectory = null, IReadOnlyDictionary<string, string> environmentVariables = null, int? timeout = null, bool? logOutput = null, bool? logInvocation = null, Func<string, string> outputFilter = null, Action<OutputType, string> customLogger = null)
        {
            using var process = ProcessTasks.StartProcess(RetypePath, arguments, workingDirectory, environmentVariables, timeout, logOutput, logInvocation, customLogger ?? RetypeLogger, outputFilter);
            process.AssertZeroExitCode();
            return process.Output;
        }
        /// <summary>
        ///   <p>Retype is an ultra-high-performance generator that builds a website based on simple text files. Focus on your writing while Retype builds the rest.</p>
        ///   <p>For more details, visit the <a href="https://retype.com">official website</a>.</p>
        /// </summary>
        /// <remarks>
        ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
        ///   <ul>
        ///     <li><c>&lt;config&gt;</c> via <see cref="RetypeBuildSettings.Config"/></li>
        ///     <li><c>--output</c> via <see cref="RetypeBuildSettings.Output"/></li>
        ///     <li><c>--override</c> via <see cref="RetypeBuildSettings.Override"/></li>
        ///     <li><c>--secret</c> via <see cref="RetypeBuildSettings.Secret"/></li>
        ///     <li><c>--verbose</c> via <see cref="RetypeBuildSettings.Verbose"/></li>
        ///   </ul>
        /// </remarks>
        public static IReadOnlyCollection<Output> RetypeBuild(RetypeBuildSettings toolSettings = null)
        {
            toolSettings = toolSettings ?? new RetypeBuildSettings();
            using var process = ProcessTasks.StartProcess(toolSettings);
            process.AssertZeroExitCode();
            return process.Output;
        }
        /// <summary>
        ///   <p>Retype is an ultra-high-performance generator that builds a website based on simple text files. Focus on your writing while Retype builds the rest.</p>
        ///   <p>For more details, visit the <a href="https://retype.com">official website</a>.</p>
        /// </summary>
        /// <remarks>
        ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
        ///   <ul>
        ///     <li><c>&lt;config&gt;</c> via <see cref="RetypeBuildSettings.Config"/></li>
        ///     <li><c>--output</c> via <see cref="RetypeBuildSettings.Output"/></li>
        ///     <li><c>--override</c> via <see cref="RetypeBuildSettings.Override"/></li>
        ///     <li><c>--secret</c> via <see cref="RetypeBuildSettings.Secret"/></li>
        ///     <li><c>--verbose</c> via <see cref="RetypeBuildSettings.Verbose"/></li>
        ///   </ul>
        /// </remarks>
        public static IReadOnlyCollection<Output> RetypeBuild(Configure<RetypeBuildSettings> configurator)
        {
            return RetypeBuild(configurator(new RetypeBuildSettings()));
        }
        /// <summary>
        ///   <p>Retype is an ultra-high-performance generator that builds a website based on simple text files. Focus on your writing while Retype builds the rest.</p>
        ///   <p>For more details, visit the <a href="https://retype.com">official website</a>.</p>
        /// </summary>
        /// <remarks>
        ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
        ///   <ul>
        ///     <li><c>&lt;config&gt;</c> via <see cref="RetypeBuildSettings.Config"/></li>
        ///     <li><c>--output</c> via <see cref="RetypeBuildSettings.Output"/></li>
        ///     <li><c>--override</c> via <see cref="RetypeBuildSettings.Override"/></li>
        ///     <li><c>--secret</c> via <see cref="RetypeBuildSettings.Secret"/></li>
        ///     <li><c>--verbose</c> via <see cref="RetypeBuildSettings.Verbose"/></li>
        ///   </ul>
        /// </remarks>
        public static IEnumerable<(RetypeBuildSettings Settings, IReadOnlyCollection<Output> Output)> RetypeBuild(CombinatorialConfigure<RetypeBuildSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false)
        {
            return configurator.Invoke(RetypeBuild, RetypeLogger, degreeOfParallelism, completeOnFailure);
        }
        /// <summary>
        ///   <p>Retype is an ultra-high-performance generator that builds a website based on simple text files. Focus on your writing while Retype builds the rest.</p>
        ///   <p>For more details, visit the <a href="https://retype.com">official website</a>.</p>
        /// </summary>
        /// <remarks>
        ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
        ///   <ul>
        ///     <li><c>&lt;config&gt;</c> via <see cref="RetypeWatchSettings.Config"/></li>
        ///     <li><c>--api</c> via <see cref="RetypeWatchSettings.WatchApi"/></li>
        ///     <li><c>--host</c> via <see cref="RetypeWatchSettings.Host"/></li>
        ///     <li><c>--port</c> via <see cref="RetypeWatchSettings.Port"/></li>
        ///     <li><c>--secret</c> via <see cref="RetypeWatchSettings.Secret"/></li>
        ///     <li><c>--verbose</c> via <see cref="RetypeWatchSettings.Verbose"/></li>
        ///   </ul>
        /// </remarks>
        public static IReadOnlyCollection<Output> RetypeWatch(RetypeWatchSettings toolSettings = null)
        {
            toolSettings = toolSettings ?? new RetypeWatchSettings();
            using var process = ProcessTasks.StartProcess(toolSettings);
            process.AssertZeroExitCode();
            return process.Output;
        }
        /// <summary>
        ///   <p>Retype is an ultra-high-performance generator that builds a website based on simple text files. Focus on your writing while Retype builds the rest.</p>
        ///   <p>For more details, visit the <a href="https://retype.com">official website</a>.</p>
        /// </summary>
        /// <remarks>
        ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
        ///   <ul>
        ///     <li><c>&lt;config&gt;</c> via <see cref="RetypeWatchSettings.Config"/></li>
        ///     <li><c>--api</c> via <see cref="RetypeWatchSettings.WatchApi"/></li>
        ///     <li><c>--host</c> via <see cref="RetypeWatchSettings.Host"/></li>
        ///     <li><c>--port</c> via <see cref="RetypeWatchSettings.Port"/></li>
        ///     <li><c>--secret</c> via <see cref="RetypeWatchSettings.Secret"/></li>
        ///     <li><c>--verbose</c> via <see cref="RetypeWatchSettings.Verbose"/></li>
        ///   </ul>
        /// </remarks>
        public static IReadOnlyCollection<Output> RetypeWatch(Configure<RetypeWatchSettings> configurator)
        {
            return RetypeWatch(configurator(new RetypeWatchSettings()));
        }
        /// <summary>
        ///   <p>Retype is an ultra-high-performance generator that builds a website based on simple text files. Focus on your writing while Retype builds the rest.</p>
        ///   <p>For more details, visit the <a href="https://retype.com">official website</a>.</p>
        /// </summary>
        /// <remarks>
        ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
        ///   <ul>
        ///     <li><c>&lt;config&gt;</c> via <see cref="RetypeWatchSettings.Config"/></li>
        ///     <li><c>--api</c> via <see cref="RetypeWatchSettings.WatchApi"/></li>
        ///     <li><c>--host</c> via <see cref="RetypeWatchSettings.Host"/></li>
        ///     <li><c>--port</c> via <see cref="RetypeWatchSettings.Port"/></li>
        ///     <li><c>--secret</c> via <see cref="RetypeWatchSettings.Secret"/></li>
        ///     <li><c>--verbose</c> via <see cref="RetypeWatchSettings.Verbose"/></li>
        ///   </ul>
        /// </remarks>
        public static IEnumerable<(RetypeWatchSettings Settings, IReadOnlyCollection<Output> Output)> RetypeWatch(CombinatorialConfigure<RetypeWatchSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false)
        {
            return configurator.Invoke(RetypeWatch, RetypeLogger, degreeOfParallelism, completeOnFailure);
        }
        /// <summary>
        ///   <p>Retype is an ultra-high-performance generator that builds a website based on simple text files. Focus on your writing while Retype builds the rest.</p>
        ///   <p>For more details, visit the <a href="https://retype.com">official website</a>.</p>
        /// </summary>
        /// <remarks>
        ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
        ///   <ul>
        ///     <li><c>&lt;config&gt;</c> via <see cref="RetypeRunSettings.Config"/></li>
        ///     <li><c>--host</c> via <see cref="RetypeRunSettings.Host"/></li>
        ///     <li><c>--port</c> via <see cref="RetypeRunSettings.Port"/></li>
        ///     <li><c>--verbose</c> via <see cref="RetypeRunSettings.Verbose"/></li>
        ///   </ul>
        /// </remarks>
        public static IReadOnlyCollection<Output> RetypeRun(RetypeRunSettings toolSettings = null)
        {
            toolSettings = toolSettings ?? new RetypeRunSettings();
            using var process = ProcessTasks.StartProcess(toolSettings);
            process.AssertZeroExitCode();
            return process.Output;
        }
        /// <summary>
        ///   <p>Retype is an ultra-high-performance generator that builds a website based on simple text files. Focus on your writing while Retype builds the rest.</p>
        ///   <p>For more details, visit the <a href="https://retype.com">official website</a>.</p>
        /// </summary>
        /// <remarks>
        ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
        ///   <ul>
        ///     <li><c>&lt;config&gt;</c> via <see cref="RetypeRunSettings.Config"/></li>
        ///     <li><c>--host</c> via <see cref="RetypeRunSettings.Host"/></li>
        ///     <li><c>--port</c> via <see cref="RetypeRunSettings.Port"/></li>
        ///     <li><c>--verbose</c> via <see cref="RetypeRunSettings.Verbose"/></li>
        ///   </ul>
        /// </remarks>
        public static IReadOnlyCollection<Output> RetypeRun(Configure<RetypeRunSettings> configurator)
        {
            return RetypeRun(configurator(new RetypeRunSettings()));
        }
        /// <summary>
        ///   <p>Retype is an ultra-high-performance generator that builds a website based on simple text files. Focus on your writing while Retype builds the rest.</p>
        ///   <p>For more details, visit the <a href="https://retype.com">official website</a>.</p>
        /// </summary>
        /// <remarks>
        ///   <p>This is a <a href="http://www.nuke.build/docs/authoring-builds/cli-tools.html#fluent-apis">CLI wrapper with fluent API</a> that allows to modify the following arguments:</p>
        ///   <ul>
        ///     <li><c>&lt;config&gt;</c> via <see cref="RetypeRunSettings.Config"/></li>
        ///     <li><c>--host</c> via <see cref="RetypeRunSettings.Host"/></li>
        ///     <li><c>--port</c> via <see cref="RetypeRunSettings.Port"/></li>
        ///     <li><c>--verbose</c> via <see cref="RetypeRunSettings.Verbose"/></li>
        ///   </ul>
        /// </remarks>
        public static IEnumerable<(RetypeRunSettings Settings, IReadOnlyCollection<Output> Output)> RetypeRun(CombinatorialConfigure<RetypeRunSettings> configurator, int degreeOfParallelism = 1, bool completeOnFailure = false)
        {
            return configurator.Invoke(RetypeRun, RetypeLogger, degreeOfParallelism, completeOnFailure);
        }
    }
    #region RetypeBuildSettings
    /// <summary>
    ///   Used within <see cref="RetypeTasks"/>.
    /// </summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    [Serializable]
    public partial class RetypeBuildSettings : ToolSettings
    {
        /// <summary>
        ///   Path to the Retype executable.
        /// </summary>
        public override string ProcessToolPath => base.ProcessToolPath ?? GetProcessToolPath();
        public override Action<OutputType, string> ProcessCustomLogger => base.ProcessCustomLogger ?? RetypeTasks.RetypeLogger;
        /// <summary>
        ///   A custom path to the output directory.
        /// </summary>
        public virtual string Output { get; internal set; }
        /// <summary>
        ///   Retype secret key.
        /// </summary>
        public virtual string Secret { get; internal set; }
        /// <summary>
        ///   JSON configuration overriding Retype config values.
        /// </summary>
        public virtual string Override { get; internal set; }
        /// <summary>
        ///   Path to the project root or a Retype config.
        /// </summary>
        public virtual string Config { get; internal set; }
        /// <summary>
        ///   Turn verbose logging on.
        /// </summary>
        public virtual bool? Verbose { get; internal set; }
        protected override Arguments ConfigureProcessArguments(Arguments arguments)
        {
            arguments
              .Add("build")
              .Add("--output {value}", Output)
              .Add("--secret {value}", Secret)
              .Add("--override {value}", Override)
              .Add("{value}", Config)
              .Add("--verbose", Verbose);
            return base.ConfigureProcessArguments(arguments);
        }
    }
    #endregion
    #region RetypeWatchSettings
    /// <summary>
    ///   Used within <see cref="RetypeTasks"/>.
    /// </summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    [Serializable]
    public partial class RetypeWatchSettings : ToolSettings
    {
        /// <summary>
        ///   Path to the Retype executable.
        /// </summary>
        public override string ProcessToolPath => base.ProcessToolPath ?? GetProcessToolPath();
        public override Action<OutputType, string> ProcessCustomLogger => base.ProcessCustomLogger ?? RetypeTasks.RetypeLogger;
        /// <summary>
        ///   Retype secret key.
        /// </summary>
        public virtual string Secret { get; internal set; }
        /// <summary>
        ///   Watch for API changes.
        /// </summary>
        public virtual bool? WatchApi { get; internal set; }
        /// <summary>
        ///   Custom host name or IP address.
        /// </summary>
        public virtual string Host { get; internal set; }
        /// <summary>
        ///   Custom TCP Port.
        /// </summary>
        public virtual int? Port { get; internal set; }
        /// <summary>
        ///   Path to the project root or a Retype config.
        /// </summary>
        public virtual string Config { get; internal set; }
        /// <summary>
        ///   Turn verbose logging on.
        /// </summary>
        public virtual bool? Verbose { get; internal set; }
        protected override Arguments ConfigureProcessArguments(Arguments arguments)
        {
            arguments
              .Add("watch")
              .Add("--secret {value}", Secret)
              .Add("--api", WatchApi)
              .Add("--host {value}", Host)
              .Add("--port {value}", Port)
              .Add("{value}", Config)
              .Add("--verbose", Verbose);
            return base.ConfigureProcessArguments(arguments);
        }
    }
    #endregion
    #region RetypeRunSettings
    /// <summary>
    ///   Used within <see cref="RetypeTasks"/>.
    /// </summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    [Serializable]
    public partial class RetypeRunSettings : ToolSettings
    {
        /// <summary>
        ///   Path to the Retype executable.
        /// </summary>
        public override string ProcessToolPath => base.ProcessToolPath ?? GetProcessToolPath();
        public override Action<OutputType, string> ProcessCustomLogger => base.ProcessCustomLogger ?? RetypeTasks.RetypeLogger;
        /// <summary>
        ///   Custom host name or IP address.
        /// </summary>
        public virtual string Host { get; internal set; }
        /// <summary>
        ///   Custom TCP Port.
        /// </summary>
        public virtual int? Port { get; internal set; }
        /// <summary>
        ///   Path to the project root or a Retype config.
        /// </summary>
        public virtual string Config { get; internal set; }
        /// <summary>
        ///   Turn verbose logging on.
        /// </summary>
        public virtual bool? Verbose { get; internal set; }
        protected override Arguments ConfigureProcessArguments(Arguments arguments)
        {
            arguments
              .Add("run")
              .Add("--host {value}", Host)
              .Add("--port {value}", Port)
              .Add("{value}", Config)
              .Add("--verbose", Verbose);
            return base.ConfigureProcessArguments(arguments);
        }
    }
    #endregion
    #region RetypeBuildSettingsExtensions
    /// <summary>
    ///   Used within <see cref="RetypeTasks"/>.
    /// </summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    public static partial class RetypeBuildSettingsExtensions
    {
        #region Output
        /// <summary>
        ///   <p><em>Sets <see cref="RetypeBuildSettings.Output"/></em></p>
        ///   <p>A custom path to the output directory.</p>
        /// </summary>
        [Pure]
        public static T SetOutput<T>(this T toolSettings, string output) where T : RetypeBuildSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Output = output;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="RetypeBuildSettings.Output"/></em></p>
        ///   <p>A custom path to the output directory.</p>
        /// </summary>
        [Pure]
        public static T ResetOutput<T>(this T toolSettings) where T : RetypeBuildSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Output = null;
            return toolSettings;
        }
        #endregion
        #region Secret
        /// <summary>
        ///   <p><em>Sets <see cref="RetypeBuildSettings.Secret"/></em></p>
        ///   <p>Retype secret key.</p>
        /// </summary>
        [Pure]
        public static T SetSecret<T>(this T toolSettings, string secret) where T : RetypeBuildSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Secret = secret;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="RetypeBuildSettings.Secret"/></em></p>
        ///   <p>Retype secret key.</p>
        /// </summary>
        [Pure]
        public static T ResetSecret<T>(this T toolSettings) where T : RetypeBuildSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Secret = null;
            return toolSettings;
        }
        #endregion
        #region Override
        /// <summary>
        ///   <p><em>Sets <see cref="RetypeBuildSettings.Override"/></em></p>
        ///   <p>JSON configuration overriding Retype config values.</p>
        /// </summary>
        [Pure]
        public static T SetOverride<T>(this T toolSettings, string @override) where T : RetypeBuildSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Override = @override;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="RetypeBuildSettings.Override"/></em></p>
        ///   <p>JSON configuration overriding Retype config values.</p>
        /// </summary>
        [Pure]
        public static T ResetOverride<T>(this T toolSettings) where T : RetypeBuildSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Override = null;
            return toolSettings;
        }
        #endregion
        #region Config
        /// <summary>
        ///   <p><em>Sets <see cref="RetypeBuildSettings.Config"/></em></p>
        ///   <p>Path to the project root or a Retype config.</p>
        /// </summary>
        [Pure]
        public static T SetConfig<T>(this T toolSettings, string config) where T : RetypeBuildSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Config = config;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="RetypeBuildSettings.Config"/></em></p>
        ///   <p>Path to the project root or a Retype config.</p>
        /// </summary>
        [Pure]
        public static T ResetConfig<T>(this T toolSettings) where T : RetypeBuildSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Config = null;
            return toolSettings;
        }
        #endregion
        #region Verbose
        /// <summary>
        ///   <p><em>Sets <see cref="RetypeBuildSettings.Verbose"/></em></p>
        ///   <p>Turn verbose logging on.</p>
        /// </summary>
        [Pure]
        public static T SetVerbose<T>(this T toolSettings, bool? verbose) where T : RetypeBuildSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Verbose = verbose;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="RetypeBuildSettings.Verbose"/></em></p>
        ///   <p>Turn verbose logging on.</p>
        /// </summary>
        [Pure]
        public static T ResetVerbose<T>(this T toolSettings) where T : RetypeBuildSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Verbose = null;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Enables <see cref="RetypeBuildSettings.Verbose"/></em></p>
        ///   <p>Turn verbose logging on.</p>
        /// </summary>
        [Pure]
        public static T EnableVerbose<T>(this T toolSettings) where T : RetypeBuildSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Verbose = true;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Disables <see cref="RetypeBuildSettings.Verbose"/></em></p>
        ///   <p>Turn verbose logging on.</p>
        /// </summary>
        [Pure]
        public static T DisableVerbose<T>(this T toolSettings) where T : RetypeBuildSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Verbose = false;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Toggles <see cref="RetypeBuildSettings.Verbose"/></em></p>
        ///   <p>Turn verbose logging on.</p>
        /// </summary>
        [Pure]
        public static T ToggleVerbose<T>(this T toolSettings) where T : RetypeBuildSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Verbose = !toolSettings.Verbose;
            return toolSettings;
        }
        #endregion
    }
    #endregion
    #region RetypeWatchSettingsExtensions
    /// <summary>
    ///   Used within <see cref="RetypeTasks"/>.
    /// </summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    public static partial class RetypeWatchSettingsExtensions
    {
        #region Secret
        /// <summary>
        ///   <p><em>Sets <see cref="RetypeWatchSettings.Secret"/></em></p>
        ///   <p>Retype secret key.</p>
        /// </summary>
        [Pure]
        public static T SetSecret<T>(this T toolSettings, string secret) where T : RetypeWatchSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Secret = secret;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="RetypeWatchSettings.Secret"/></em></p>
        ///   <p>Retype secret key.</p>
        /// </summary>
        [Pure]
        public static T ResetSecret<T>(this T toolSettings) where T : RetypeWatchSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Secret = null;
            return toolSettings;
        }
        #endregion
        #region WatchApi
        /// <summary>
        ///   <p><em>Sets <see cref="RetypeWatchSettings.WatchApi"/></em></p>
        ///   <p>Watch for API changes.</p>
        /// </summary>
        [Pure]
        public static T SetWatchApi<T>(this T toolSettings, bool? watchApi) where T : RetypeWatchSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.WatchApi = watchApi;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="RetypeWatchSettings.WatchApi"/></em></p>
        ///   <p>Watch for API changes.</p>
        /// </summary>
        [Pure]
        public static T ResetWatchApi<T>(this T toolSettings) where T : RetypeWatchSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.WatchApi = null;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Enables <see cref="RetypeWatchSettings.WatchApi"/></em></p>
        ///   <p>Watch for API changes.</p>
        /// </summary>
        [Pure]
        public static T EnableWatchApi<T>(this T toolSettings) where T : RetypeWatchSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.WatchApi = true;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Disables <see cref="RetypeWatchSettings.WatchApi"/></em></p>
        ///   <p>Watch for API changes.</p>
        /// </summary>
        [Pure]
        public static T DisableWatchApi<T>(this T toolSettings) where T : RetypeWatchSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.WatchApi = false;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Toggles <see cref="RetypeWatchSettings.WatchApi"/></em></p>
        ///   <p>Watch for API changes.</p>
        /// </summary>
        [Pure]
        public static T ToggleWatchApi<T>(this T toolSettings) where T : RetypeWatchSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.WatchApi = !toolSettings.WatchApi;
            return toolSettings;
        }
        #endregion
        #region Host
        /// <summary>
        ///   <p><em>Sets <see cref="RetypeWatchSettings.Host"/></em></p>
        ///   <p>Custom host name or IP address.</p>
        /// </summary>
        [Pure]
        public static T SetHost<T>(this T toolSettings, string host) where T : RetypeWatchSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Host = host;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="RetypeWatchSettings.Host"/></em></p>
        ///   <p>Custom host name or IP address.</p>
        /// </summary>
        [Pure]
        public static T ResetHost<T>(this T toolSettings) where T : RetypeWatchSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Host = null;
            return toolSettings;
        }
        #endregion
        #region Port
        /// <summary>
        ///   <p><em>Sets <see cref="RetypeWatchSettings.Port"/></em></p>
        ///   <p>Custom TCP Port.</p>
        /// </summary>
        [Pure]
        public static T SetPort<T>(this T toolSettings, int? port) where T : RetypeWatchSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Port = port;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="RetypeWatchSettings.Port"/></em></p>
        ///   <p>Custom TCP Port.</p>
        /// </summary>
        [Pure]
        public static T ResetPort<T>(this T toolSettings) where T : RetypeWatchSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Port = null;
            return toolSettings;
        }
        #endregion
        #region Config
        /// <summary>
        ///   <p><em>Sets <see cref="RetypeWatchSettings.Config"/></em></p>
        ///   <p>Path to the project root or a Retype config.</p>
        /// </summary>
        [Pure]
        public static T SetConfig<T>(this T toolSettings, string config) where T : RetypeWatchSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Config = config;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="RetypeWatchSettings.Config"/></em></p>
        ///   <p>Path to the project root or a Retype config.</p>
        /// </summary>
        [Pure]
        public static T ResetConfig<T>(this T toolSettings) where T : RetypeWatchSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Config = null;
            return toolSettings;
        }
        #endregion
        #region Verbose
        /// <summary>
        ///   <p><em>Sets <see cref="RetypeWatchSettings.Verbose"/></em></p>
        ///   <p>Turn verbose logging on.</p>
        /// </summary>
        [Pure]
        public static T SetVerbose<T>(this T toolSettings, bool? verbose) where T : RetypeWatchSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Verbose = verbose;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="RetypeWatchSettings.Verbose"/></em></p>
        ///   <p>Turn verbose logging on.</p>
        /// </summary>
        [Pure]
        public static T ResetVerbose<T>(this T toolSettings) where T : RetypeWatchSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Verbose = null;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Enables <see cref="RetypeWatchSettings.Verbose"/></em></p>
        ///   <p>Turn verbose logging on.</p>
        /// </summary>
        [Pure]
        public static T EnableVerbose<T>(this T toolSettings) where T : RetypeWatchSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Verbose = true;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Disables <see cref="RetypeWatchSettings.Verbose"/></em></p>
        ///   <p>Turn verbose logging on.</p>
        /// </summary>
        [Pure]
        public static T DisableVerbose<T>(this T toolSettings) where T : RetypeWatchSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Verbose = false;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Toggles <see cref="RetypeWatchSettings.Verbose"/></em></p>
        ///   <p>Turn verbose logging on.</p>
        /// </summary>
        [Pure]
        public static T ToggleVerbose<T>(this T toolSettings) where T : RetypeWatchSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Verbose = !toolSettings.Verbose;
            return toolSettings;
        }
        #endregion
    }
    #endregion
    #region RetypeRunSettingsExtensions
    /// <summary>
    ///   Used within <see cref="RetypeTasks"/>.
    /// </summary>
    [PublicAPI]
    [ExcludeFromCodeCoverage]
    public static partial class RetypeRunSettingsExtensions
    {
        #region Host
        /// <summary>
        ///   <p><em>Sets <see cref="RetypeRunSettings.Host"/></em></p>
        ///   <p>Custom host name or IP address.</p>
        /// </summary>
        [Pure]
        public static T SetHost<T>(this T toolSettings, string host) where T : RetypeRunSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Host = host;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="RetypeRunSettings.Host"/></em></p>
        ///   <p>Custom host name or IP address.</p>
        /// </summary>
        [Pure]
        public static T ResetHost<T>(this T toolSettings) where T : RetypeRunSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Host = null;
            return toolSettings;
        }
        #endregion
        #region Port
        /// <summary>
        ///   <p><em>Sets <see cref="RetypeRunSettings.Port"/></em></p>
        ///   <p>Custom TCP Port.</p>
        /// </summary>
        [Pure]
        public static T SetPort<T>(this T toolSettings, int? port) where T : RetypeRunSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Port = port;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="RetypeRunSettings.Port"/></em></p>
        ///   <p>Custom TCP Port.</p>
        /// </summary>
        [Pure]
        public static T ResetPort<T>(this T toolSettings) where T : RetypeRunSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Port = null;
            return toolSettings;
        }
        #endregion
        #region Config
        /// <summary>
        ///   <p><em>Sets <see cref="RetypeRunSettings.Config"/></em></p>
        ///   <p>Path to the project root or a Retype config.</p>
        /// </summary>
        [Pure]
        public static T SetConfig<T>(this T toolSettings, string config) where T : RetypeRunSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Config = config;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="RetypeRunSettings.Config"/></em></p>
        ///   <p>Path to the project root or a Retype config.</p>
        /// </summary>
        [Pure]
        public static T ResetConfig<T>(this T toolSettings) where T : RetypeRunSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Config = null;
            return toolSettings;
        }
        #endregion
        #region Verbose
        /// <summary>
        ///   <p><em>Sets <see cref="RetypeRunSettings.Verbose"/></em></p>
        ///   <p>Turn verbose logging on.</p>
        /// </summary>
        [Pure]
        public static T SetVerbose<T>(this T toolSettings, bool? verbose) where T : RetypeRunSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Verbose = verbose;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Resets <see cref="RetypeRunSettings.Verbose"/></em></p>
        ///   <p>Turn verbose logging on.</p>
        /// </summary>
        [Pure]
        public static T ResetVerbose<T>(this T toolSettings) where T : RetypeRunSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Verbose = null;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Enables <see cref="RetypeRunSettings.Verbose"/></em></p>
        ///   <p>Turn verbose logging on.</p>
        /// </summary>
        [Pure]
        public static T EnableVerbose<T>(this T toolSettings) where T : RetypeRunSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Verbose = true;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Disables <see cref="RetypeRunSettings.Verbose"/></em></p>
        ///   <p>Turn verbose logging on.</p>
        /// </summary>
        [Pure]
        public static T DisableVerbose<T>(this T toolSettings) where T : RetypeRunSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Verbose = false;
            return toolSettings;
        }
        /// <summary>
        ///   <p><em>Toggles <see cref="RetypeRunSettings.Verbose"/></em></p>
        ///   <p>Turn verbose logging on.</p>
        /// </summary>
        [Pure]
        public static T ToggleVerbose<T>(this T toolSettings) where T : RetypeRunSettings
        {
            toolSettings = toolSettings.NewInstance();
            toolSettings.Verbose = !toolSettings.Verbose;
            return toolSettings;
        }
        #endregion
    }
    #endregion
}
