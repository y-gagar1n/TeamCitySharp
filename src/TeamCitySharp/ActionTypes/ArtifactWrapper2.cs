using System.Collections.Generic;
using TeamCitySharp.Connection;
using TeamCitySharp.DomainEntities;

namespace TeamCitySharp.ActionTypes
{
    /// <remarks>
    /// This class is only supported by TeamCity 8.x and higher.
    /// </remarks>
    internal sealed class ArtifactWrapper2 : IArtifactWrapper2
    {
        private readonly ITeamCityCaller _caller;
        private readonly Artifacts _artifacts;
        private readonly string _artifactRelativeName;

        public ArtifactWrapper2(ITeamCityCaller caller, Artifacts artifacts, string artifactRelativeName)
        {
            _caller = caller;
            _artifacts = artifacts;
            _artifactRelativeName = artifactRelativeName;
        }

        /// <summary>
        /// The published artifacts
        /// </summary>
        public List<Artifact> Artifacts { get { return _artifacts.Files; } }

        /// <summary>
        /// Gets download Urls for the associated artifacts.
        /// </summary>
        /// <param name="extractArchives">
        /// If <see langword="true"/> files contained within archives will be listed individually.
        /// </param>
        /// <remarks>
        /// This method is only supported by TeamCity 8.x and higher.
        /// </remarks>
        public List<string> GetDownloadUrls(bool extractArchives = false)
        {
            var downloader = new ArtifactDownloader(_caller);
            return downloader.GetDownloadUrls(_artifacts, _artifactRelativeName, extractArchives);
        }
    }
}
