using System;
using System.Threading.Tasks;

namespace KTranslate.Update
{
    public interface IReleasesClient
    {
        Task<Version> GetLastVersionAsync();
    }
}
