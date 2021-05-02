import { Component, OnInit } from '@angular/core';
import { PoListViewAction, PoNotificationService, PoPageFilter } from '@po-ui/ng-components';
import { Opening } from '../opening-management-page/models/opening.model';
import { OpeningListingPageStateService } from './opening-listing-page-state.service';

@Component({
  selector: 'app-opening-listing-page',
  templateUrl: './opening-listing-page.component.html',
  styleUrls: ['./opening-listing-page.component.css'],
  providers: [OpeningListingPageStateService]
})
export class OpeningListingPageComponent implements OnInit {
  filter: PoPageFilter = {
    action: (filter: string) => this.pageStateService.setFilter(filter),
    placeholder: 'Filter by name'
  };
  state$ = this.pageStateService.state$;
  listViewActions: PoListViewAction[] = [
    {
      label: 'Apply',
      action: async (opening: Opening) => {
        if (opening.id) {
          await this.pageStateService.apply(opening.id);
          this.notification.success('You successfully applied to the opening.');
        }
      }
    }
  ];
  constructor(private pageStateService: OpeningListingPageStateService, private notification: PoNotificationService) { }

  async ngOnInit(): Promise<void> {
    await this.pageStateService.init();
  }

}
