using System;
using Stylet;
using StyletIoC;
using WPFToDo.GUI.Pages;
using WPFToDo.Database;

namespace WPFToDo.GUI
{
    public class Bootstrapper : Bootstrapper<ShellViewModel>
    {
        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            base.ConfigureIoC(builder);
            //builder.Bind<IToDoStore>().To<DapperRespository>();
        }

        protected override void Configure()
        {
            // Perform any other configuration before the application starts
        }
    }
}
