using EF_Core.Service;
using System.Configuration;
using System.Data;
using System.Windows;

namespace EF_Core
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static UserService UserService { get; } = new UserService();
    }

}
