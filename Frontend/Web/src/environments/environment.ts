const backendUrl = import.meta.env['NG_APP_BACKEND_URL'];
const cmsUrl = import.meta.env['NG_APP_CMS_URL'];

export const environment = {
  production: true,
  backendUrl: backendUrl,
  cmsUrl: cmsUrl,
};
