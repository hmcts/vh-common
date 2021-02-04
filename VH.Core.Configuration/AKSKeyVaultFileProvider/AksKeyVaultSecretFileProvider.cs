using System;
using System.IO;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;

namespace VH.Core.Configuration.AKSKeyVaultFileProvider
{
    /// <summary>
    /// Based on the .NET Physical File Provider.
    /// https://docs.microsoft.com/en-us/dotnet/api/microsoft.extensions.fileproviders.physicalfileprovider?view=dotnet-plat-ext-5.0
    /// </summary>
    public class AksKeyVaultSecretFileProvider : IFileProvider
    {
        private readonly string _rootPath;

        public static readonly char[] PathSeparators =
        {
            Path.DirectorySeparatorChar,
            Path.AltDirectorySeparatorChar
        };

        public AksKeyVaultSecretFileProvider(string rootPath)
        {
            _rootPath = Path.GetFullPath(rootPath);
            if (!Directory.Exists(_rootPath))
            {
                throw new DirectoryNotFoundException(_rootPath);
            }
        }

        private string GetFullPath(string path)
        {
            string fullPath;
            try
            {
                fullPath = Path.GetFullPath(Path.Combine(_rootPath, path));
            }
            catch
            {
                return null;
            }

            if (!IsUnderneathRoot(fullPath))
            {
                return null;
            }

            return fullPath;
        }

        private bool IsUnderneathRoot(string fullPath)
        {
            return fullPath.StartsWith(_rootPath, StringComparison.OrdinalIgnoreCase);
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            if (string.IsNullOrWhiteSpace(subpath))
            {
                return new NotFoundFileInfo(subpath);
            }

            subpath = subpath.TrimStart(PathSeparators);
            if (Path.IsPathRooted(subpath))
            {
                return new NotFoundFileInfo(subpath);
            }

            var fullPath = GetFullPath(subpath);
            if (fullPath == null)
            {
                return new NotFoundFileInfo(subpath);
            }

            var fileInfo = new FileInfo(fullPath);
            return new AksKeyVaultSecretFileInfo(fileInfo);
        }

        public IDirectoryContents GetDirectoryContents(string subpath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(subpath))
                {
                    return NotFoundDirectoryContents.Singleton;
                }

                subpath = subpath.TrimStart(PathSeparators);
                if (Path.IsPathRooted(subpath))
                {
                    return NotFoundDirectoryContents.Singleton;
                }

                var fullPath = GetFullPath(subpath);
                if (fullPath == null || !Directory.Exists(fullPath))
                {
                    return NotFoundDirectoryContents.Singleton;
                }

                return new AksKeyVaultSecretDirectoryContents(fullPath);
            }
            catch (DirectoryNotFoundException)
            {
                return NotFoundDirectoryContents.Singleton;
            }
            catch (IOException)
            {
                return NotFoundDirectoryContents.Singleton;
            }
        }

        public IChangeToken Watch(string filter)
        {
            return NullChangeToken.Singleton;
        }
    }
}
