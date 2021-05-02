import { Injectable } from '@angular/core';
import { PoMenuItem } from '@po-ui/ng-components';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthService, Role } from './auth.service';

interface MenuItem extends PoMenuItem {
  subItems?: Array<MenuItem>;
  permissions: Role[];
}

const menu: MenuItem[] = [
  {
    label: 'My resume',
    link: 'resume',
    permissions: [Role.Candidate],
    icon: 'po-icon-document-filled',
  },
  {
    label: 'Openings',
    icon: 'po-icon-grid',
    permissions: [],
    subItems: [
      {
        label: 'Management',
        permissions: [Role.Recruiter],
        link: 'opening/management',
      },
    ],
  },
];

@Injectable({
  providedIn: 'root',
})
export class MenuService {
  menu$: Observable<MenuItem[]> = this.authService.state$.pipe(
    map((user) => {
      if (!user) {
        return [];
      }
      return this.getMenus(user.roles, menu);
    })
  );

  constructor(private authService: AuthService) {}

  private getMenus(roles: Role[], menuItems: MenuItem[]): MenuItem[] {
    const result: MenuItem[] = [];
    for (const item of menuItems) {
      const canAccess =
        !item.permissions ||
        item.permissions.length === 0 ||
        this.authService.canLoggedUserAccess(item.permissions);
      if (!canAccess) {
        continue;
      }
      let subItems: MenuItem[] | undefined;
      if (item.subItems && item.subItems.length > 0) {
        subItems = this.getMenus(roles, item.subItems);
        if (subItems.length === 0) {
          continue;
        }
      }
      result.push({ ...item, subItems });
    }
    return result;
  }
}
