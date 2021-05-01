import { Host } from '../app/utils/host';

export const environment = {
  production: true,
  authorityEndpoint: `https://${Host.getTenantName()}.rac.totvs.app/totvs.rac`,
  clientId: 'ats_oidc'
};
