﻿using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Final1.Authorization
{
    public class Final1AuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //Common permissions
            var manageUserPermission = context.GetPermissionOrNull(PermissionNames.ManageUserPermission);
            if (manageUserPermission == null)
            {
                manageUserPermission = context.CreatePermission(PermissionNames.ManageUserPermission, L("ManageUserPermission"));
            }


            var pages = context.GetPermissionOrNull(PermissionNames.Pages);
            if (pages == null)
            {
                pages = context.CreatePermission(PermissionNames.Pages, L("Pages"));
            }

            var users = pages.CreateChildPermission(PermissionNames.Pages_Users, L("Users"));

            //Host permissions
            var tenants = pages.CreateChildPermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, Final1Consts.LocalizationSourceName);
        }
    }
}
