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
        private readonly string rootPath;

        private static readonly char[] _pathSeparators = new[]
        {
            Path.DirectorySeparatorChar,
            Path.AltDirectorySeparatorChar
        };

        public AksKeyVaultSecretFileProvider(string _root)
        {
            rootPath = Path.GetFullPath(_root);
            if (!Directory.Exists(rootPath))
            {
                throw new DirectoryNotFoundException(rootPath);
            }
        }

        private string GetFullPath(string path)
        {
            string fullPath;
            try
            {
                fullPath = Path.GetFullPath(Path.Combine(rootPath, path));
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
            return fullPath.StartsWith(rootPath, StringComparison.OrdinalIgnoreCase);
        }

        public IFileInfo GetFileInfo(string subpath)
        {
            if (string.IsNullOrEmpty(subpath))
            {
                return new NotFoundFileInfo(subpath);
            }

            subpath = subpath.TrimStart(_pathSeparators);
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
                if (subpath == null)
                {
                    return NotFoundDirectoryContents.Singleton;
                }

                subpath = subpath.TrimStart(_pathSeparators);
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

        public IChangeToken Watch(string filter) => NullChangeToken.Singleton;
    }
}