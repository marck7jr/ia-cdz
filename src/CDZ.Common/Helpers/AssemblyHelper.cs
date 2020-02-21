using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace CDZ.Common.Helpers
{
    public static class AssemblyHelper
    {
        public static async Task<string> GetEmbeddedResourceAsync(string manifestResourceName)
        {
            if (string.IsNullOrWhiteSpace(manifestResourceName))
            {
                throw new ArgumentNullException(nameof(manifestResourceName));
            }

            var stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream(manifestResourceName);
            using (var reader = new StreamReader(stream))
            {
                return await reader
                    .ReadToEndAsync();
            }

        }
    }
}
