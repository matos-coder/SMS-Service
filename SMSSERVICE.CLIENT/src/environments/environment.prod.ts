import packageInfo from '../../package.json';

export const environment = {
  appVersion: packageInfo.version,
  production: true,
  baseUrl:'http://localhost:5267/api',
  assetUrl :'http://localhost:5267'
};
