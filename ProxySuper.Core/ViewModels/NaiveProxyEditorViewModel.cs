﻿using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using ProxySuper.Core.Models;
using ProxySuper.Core.Models.Hosts;
using ProxySuper.Core.Models.Projects;
using ProxySuper.Core.Services;

namespace ProxySuper.Core.ViewModels
{
    public class NaiveProxyEditorViewModel : MvxViewModel<Record, Record>
    {
        public NaiveProxyEditorViewModel(IMvxNavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public IMvxNavigationService NavigationService { get; }

        public string Id { get; set; }

        public Host Host { get; set; }

        public NaiveProxySettings Settings { get; set; }

        public override void Prepare(Record parameter)
        {
            var record = Utils.DeepClone(parameter);

            Id = record.Id;
            Host = record.Host;
            Settings = record.NaiveProxySettings;
        }


        public IMvxCommand SaveCommand => new MvxCommand(Save);

        public IMvxCommand SaveAndInstallCommand => new MvxCommand(SaveAndInstall);

        private void Save()
        {
            NavigationService.Close(this, new Record
            {
                Id = Id,
                Host = Host,
                NaiveProxySettings = Settings
            });
        }


        private void SaveAndInstall()
        {
            var record = new Record
            {
                Id = this.Id,
                Host = this.Host,
                NaiveProxySettings = Settings,
            };
            NavigationService.Close(this, record);
            NavigationService.Navigate<NaiveProxyInstallViewModel, Record>(record);
        }
    }
}
