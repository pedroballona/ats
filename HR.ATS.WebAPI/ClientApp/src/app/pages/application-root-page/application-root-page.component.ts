import { Component } from '@angular/core';
import { PoToolbarAction, PoToolbarProfile } from '@po-ui/ng-components';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { AuthService } from '../../utils/auth.service';
import { MenuService } from '../../utils/menu.service';

@Component({
  selector: 'app-application-root-page',
  templateUrl: './application-root-page.component.html',
  styleUrls: ['./application-root-page.component.css'],
})
export class ApplicationRootPageComponent {
  constructor(
    private authService: AuthService,
    private menuService: MenuService
  ) {}

  readonly menus$ = this.menuService.menu$;
  readonly profile$: Observable<
    PoToolbarProfile | undefined
  > = this.authService.state$.pipe(
    map((user) => {
      if (!user) {
        return undefined;
      }
      return { title: user.name };
    })
  );
  readonly profileActions: PoToolbarAction[] = [
    {
      label: 'Sair',
      action: () => {
        this.authService.logout();
      }
    }
  ];
}
