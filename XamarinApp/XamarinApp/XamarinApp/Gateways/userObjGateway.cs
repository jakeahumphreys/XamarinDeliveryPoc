using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace XamarinApp.Gateways
{
    public class userObjGateway
    {
        SQLiteConnection conn = DependencyService.Get<ISql>().GetConnection();
    }
}
