using System;
using System.IO;

namespace VH.Core.Configuration.UnitTests.AKSKeyVaultFileProvider
{
    public static class TempFolderFactory
    {
        public static string CreateExistingTempFolder()
        {
            return GetTempFolderPath("exists_", true);
        }

        public static string CreateNonExistingTempFolder()
        {
            return GetTempFolderPath("does_not_exist", false);
        }

        private static string GetTempFolderPath(string folderName, bool createDirectory)
        {
            var random = Guid.NewGuid().ToString("N").Substring(0, 4);
            var path = Path.Combine(Path.GetTempPath(), folderName + random);
            if (createDirectory)
            {
                Directory.CreateDirectory(path);
            }

            return path;
        }
    }
}
