export const environment = {
  production: false,
  backendUrl: import.meta.env['NG_APP_BACKEND_URL'],
  cmsUrl: import.meta.env['NG_APP_CMS_URL'],
  keycloakUrl: import.meta.env['NG_APP_KEYCLOAK_URL'],
  keycloakRealm: import.meta.env['NG_APP_KEYCLOAK_REALM'],
  keycloakClientId: import.meta.env['NG_APP_KEYCLOAK_CLIENT_ID'],
};
