﻿#if !ONPREMISES
using OfficeDevPnP.Core.Framework.Provisioning.Model;
using SharePointPnP.PowerShell.CmdletHelpAttributes;
using System.Linq;
using System.Management.Automation;

namespace SharePointPnP.PowerShell.Commands.Provisioning
{
    [Cmdlet(VerbsCommon.New, "PnPProvisioningTeamNoGroupSite", SupportsShouldProcess = true)]
    [CmdletHelp("Creates a team site without an Office 365 group object",
        Category = CmdletHelpCategory.Provisioning, SupportedPlatform = CmdletSupportedPlatform.Online)]
    [CmdletExample(
       Code = @"PS:> $site = New-PnPProvisioningTeamNoGroupSite -Alias ""MyTeamSite"" -Title ""My Team Site""",
       Remarks = "Creates a new team site object with the specified variables",
       SortOrder = 1)]
    public class NewProvisioningTeamNoGroupSite : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Url;
        
        [Parameter(Mandatory = true)]
        public string Title;

        [Parameter(Mandatory = true)]
        public uint TimeZoneId;

        [Parameter(Mandatory = false)]
        public uint Language;

        [Parameter(Mandatory = false)]
        public string Owner;

        [Parameter(Mandatory = false)]
        public string Description;

        [Parameter(Mandatory = false)]
        public SwitchParameter HubSite;

        [Parameter(Mandatory = false)]
        public string[] TemplateIds;

        protected override void ProcessRecord()
        {
            var site = new TeamNoGroupSiteCollection
            {
                Url = Url,
                Language = (int)Language,
                Owner = Owner,
                TimeZoneId = (int)TimeZoneId,
                Description = Description,
                IsHubSite = HubSite.IsPresent,
                Title = Title
            };
            if (TemplateIds != null)
            {
                site.Templates.AddRange(TemplateIds.ToList());
            }
            WriteObject(site);
        }
    }
}
#endif