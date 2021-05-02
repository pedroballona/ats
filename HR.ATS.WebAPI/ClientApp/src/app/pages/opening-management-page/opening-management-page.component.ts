import { Component, OnInit, ViewChild } from '@angular/core';
import { PoListViewAction, PoPageAction, PoPageFilter } from '@po-ui/ng-components';
import { OpeningFormComponent } from './components/opening-form/opening-form.component';
import { Opening } from './models/opening.model';
import { OpeningManagementPageStateService } from './opening-management-page-state.service';

@Component({
  selector: 'app-opening-management-page',
  templateUrl: './opening-management-page.component.html',
  styleUrls: ['./opening-management-page.component.css'],
  providers: [OpeningManagementPageStateService]
})
export class OpeningManagementPageComponent implements OnInit {
  @ViewChild(OpeningFormComponent)
  creationModal?: OpeningFormComponent;
  actions: PoPageAction[] = [
    {
      label: 'Create opening',
      action: () => this.creationModal?.open()
    }
  ];
  filter: PoPageFilter = {
    action: (filter: string) => this.pageStateService.setFilter(filter),
    placeholder: 'Filter by name'
  };
  state$ = this.pageStateService.state$;
  listViewActions: PoListViewAction[] = [
    {
      label: 'Remover',
      action: async (opening: Opening) => {
        if (opening.id) {
          await this.pageStateService.delete(opening.id);
        }
      },
      type: 'danger'
    }
  ];

  constructor(private pageStateService: OpeningManagementPageStateService) { }

  async ngOnInit(): Promise<void> {
    await this.pageStateService.init();
  }

  async onSave(opening: Opening): Promise<void> {
    await this.pageStateService.save(opening);
    this.creationModal?.close();
  }

}
