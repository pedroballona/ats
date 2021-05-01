export class Host {

  static getHost(): string {
    const url = document.getElementsByTagName('base')[0].href;
    return url.replace(/\/$/, '');
  }

  static getTenantName(locationHost?: string): string {

    if (!locationHost) {
      locationHost = Host.getLocationHost();
    }

    if (locationHost.includes('.')) {
      return locationHost.split('.')[0];
    }
    return '';
  }

  static getLocationHost(): string {
    return location.host;
  }
}
