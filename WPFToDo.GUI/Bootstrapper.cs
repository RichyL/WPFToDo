using System;
using Stylet;
using StyletIoC;
using WPFToDo.GUI.Pages;
using WPFToDo.Database;
using System.Reflection;
using System.Linq;
using System.IO;

namespace WPFToDo.GUI
{
    public class Bootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            base.ConfigureIoC(builder);
            //builder.Bind<IToDoStore>().To<DapperRespository>();

            string wanted_path = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

        }

        protected override void Configure()
        {
            // Perform any other configuration before the application starts
        }
    }
}
