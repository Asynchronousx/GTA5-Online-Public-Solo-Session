using System;
using NetFwTypeLib;

namespace MainWindowApp { 
    public class GTA5Session {

        //Global univoque identifier (guid) necessary to identify the firewall object class.
        //Those identifier are necessary to ask the firewall to add our custom rules
        const string guidFWPolicy2 = "{E2B3C97F-6AE1-41AC-817A-F6F92166D7DD}";
        const string guidRWRule = "{2C5BC43E-3369-4C33-AB0C-BE9469677AF4}";

        //Creating the Object needed later on to work on the new rule and apply that rule
        //on the firewall
        Type typeFWPolicy2 = Type.GetTypeFromCLSID(new Guid(guidFWPolicy2));
        Type typeFWRule = Type.GetTypeFromCLSID(new Guid(guidRWRule));

        public void Block() {
            //Creating the istances of the fwPolicy and newRule.
            //fwPolicy is the actual Firewall rules activator, newRule contains all the rules
            //necessary to the program to block successfully the PORT on which the GTA Servers 
            //rely on
            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(typeFWPolicy2);
            INetFwRule newRule = (INetFwRule)Activator.CreateInstance(typeFWRule);

            //Setting the rules property 
            newRule.Name = "GTA5 Solo Public Session";
            newRule.Description = "Block some ports to get into an online session alone";

            //Since the ports we're working on rely on the UDP protocol, we need to specify that
            newRule.Protocol = (int)NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP;

            //Ports that will be blocked for the sake of the solo sessions
            newRule.LocalPorts = "6672,61455,61457,61456,61458";

            //We're blocking those ports only by the outbond traffic.
            newRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;

            //remaining stuff: setting it active, specifing the grouping procotol, setting the current
            //using fw profile and setting the action to BLOCK, to block the outbond traffic
            newRule.Enabled = true;
            newRule.Grouping = "@firewallapi.dll,-23255";
            newRule.Profiles = fwPolicy2.CurrentProfileTypes;
            newRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;

            //adding the new rule created to the firewall policy obj: that will enable the solo session
            fwPolicy2.Rules.Add(newRule);
        }

        public void Unblock() { 
            //generating another fwpolicy
            INetFwPolicy2 fwPolicy2 = (INetFwPolicy2)Activator.CreateInstance(typeFWPolicy2);

            //removing the rules created with the Block method.
            fwPolicy2.Rules.Remove("GTA5 Solo Public Session");
        }
    }
}