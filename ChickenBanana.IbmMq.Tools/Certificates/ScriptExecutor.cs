using ChickenBanana.IbmMq.Tools.Utils;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChickenBanana.IbmMq.Tools.Certificates
{
    /// <summary>
    /// Runs generated MQSC scripts.
    /// </summary>
    public class ScriptExecutor
    {
        private readonly ScriptExecutorOptions _options;
        private readonly ILogger _logger;

        public ScriptExecutor(ScriptExecutorOptions options, ILogger logger)
        {
            _options = options;
            _logger = logger;
        }

        public async Task ExecuteScriptAsync(string queueManager, string script, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(queueManager))
            {
                throw new System.ArgumentException($"'{nameof(queueManager)}' cannot be null or empty.", nameof(queueManager));
            }

            if (string.IsNullOrEmpty(script))
            {
                throw new System.ArgumentException($"'{nameof(script)}' cannot be null or empty.", nameof(script));
            }

            var scriptFileName = Path.GetTempFileName();
            await File.WriteAllTextAsync(scriptFileName, script, cancellationToken);

            var fullPath = Path.Join(_options.IbmMqClientFolder, "runmqsc.exe");
            var arguments = $" {queueManager} < {scriptFileName}";

            var exitcode = await ExecuteProcessAsync(fullPath, arguments);

            if (exitcode == 0)
            {
                File.Delete(scriptFileName);
            }

            if (exitcode != 0)
            {

                _logger.LogError($"Could not execute script {script} with command {fullPath} {arguments}");
                throw new ScriptExecutionException(exitcode);
            }
        }

        private async Task<int> ExecuteProcessAsync(string filename, string arguments)
        {
            PermissionChecker.CheckAdministrativeRights();

            var taskCompletionSource = new TaskCompletionSource<int>();

            var startInfo = new ProcessStartInfo
            {
                FileName = filename,
                Arguments = arguments,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false,                
            };          

            var process = new Process
            {
                StartInfo = startInfo,
                EnableRaisingEvents = true,                
            };

            process.Exited += (sender, args) =>
            {
                taskCompletionSource.SetResult(process.ExitCode);
                process.Dispose();
            };
          
            var started  = process.Start();

            var logStringBuilder = new StringBuilder();

            while (!process.StandardOutput.EndOfStream && started)
            {
                var line = process.StandardOutput.ReadLine();
                logStringBuilder.AppendLine(line);
            }

            _logger.LogDebug(logStringBuilder.ToString());

            return await taskCompletionSource.Task;
        }

       
    }
}
