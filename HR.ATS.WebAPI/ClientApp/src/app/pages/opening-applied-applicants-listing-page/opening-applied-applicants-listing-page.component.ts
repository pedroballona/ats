import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  PoBreadcrumb,
  PoPageFilter,
  PoTableAction,
  PoTableColumn
} from '@po-ui/ng-components';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';
import { assert } from '../../utils/assert';
import { SimpleApplicant } from './models/simple-applicant.model';
import { OpeningAppliedApplicantsListingPageStateService } from './opening-applied-applicants-listing-page-state.service';

@Component({
  selector: 'app-opening-applied-applicants-listing-page',
  templateUrl: './opening-applied-applicants-listing-page.component.html',
  styleUrls: ['./opening-applied-applicants-listing-page.component.css'],
  providers: [OpeningAppliedApplicantsListingPageStateService],
})
export class OpeningAppliedApplicantsListingPageComponent
  implements OnInit, OnDestroy {
  private destroySubject = new Subject<void>();
  state$ = this.pageStateService.state$;
  filter: PoPageFilter = {
    action: (filter: string) => this.pageStateService.setFilter(filter),
    placeholder: 'Filter by name',
  };
  columns: PoTableColumn[] = [
    {
      label: 'Applicant Name',
      property: 'name',
    },
  ];
  breadcrumb: PoBreadcrumb = {
    items: [
      {
        label: 'Opening management',
        link: '/opening/management',
      },
      {
        label: 'Applied applicants',
      },
    ],
  };
  tableActions: PoTableAction[] = [
    {
      label: 'View resume',
      action: (simpleApplicant: SimpleApplicant) => {
        assert(simpleApplicant?.id);
        this.router.navigate([
          'opening',
          'management',
          this.openingId,
          'applied',
          'applicants',
          simpleApplicant.id,
          'resume',
        ]);
      },
    },
  ];
  openingId?: string;

  constructor(
    private pageStateService: OpeningAppliedApplicantsListingPageStateService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.activatedRoute.params.pipe(takeUntil(this.destroySubject)).subscribe({
      next: async (params) => {
        this.openingId = params.id;
        assert(this.openingId);
        await this.pageStateService.init(this.openingId);
      },
    });
  }

  ngOnDestroy(): void {
    this.destroySubject.next();
  }
}
