using System.Threading.Tasks;
using Limxc.Tools.Vision.Models;
using Refit;

namespace TestHost.ApiInterface;

public interface IVision
{
    [Post("/Vision")]
    Task<MetaImage> TurnGray([Body] MetaImage meta); 
}