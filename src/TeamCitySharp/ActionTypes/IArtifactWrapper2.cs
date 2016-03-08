using System.Collections.Generic;
using TeamCitySharp.DomainEntities;

namespace TeamCitySharp.ActionTypes
{
    /// <remarks>
    /// This class is only supported by TeamCity 8.x and higher.
    /// </remarks>
    public interface IArtifactWrapper2
    {
        /// <summary>
        /// The published artifacts
        /// </summary>
        List<Artifact> Artifacts { get; }

        /// <summary>
        /// Gets download Urls for the associated artifacts.
        /// </summary>
        /// <param name="extractArchives">
        /// If <see langword="true"/> files contained within archives will be downloaded individually under a directory with the archive's name.
        /// </param>
        /// <remarks>
        /// This method is only supported by TeamCity 8.x and higher.
        /// </remarks>
        List<string> GetDownloadUrls(bool extractArchives = false);
    }
}