import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PoBreadcrumb } from '@po-ui/ng-components';
import { assert } from '../../utils/assert';
import { ResumeForRecruiterPageStateService } from './resume-for-recruiter-page-state.service';

@Component({
  selector: 'app-resume-for-recruiter-page',
  templateUrl: './resume-for-recruiter-page.component.html',
  styleUrls: ['./resume-for-recruiter-page.component.css'],
  providers: [ResumeForRecruiterPageStateService]
})
export class ResumeForRecruiterPageComponent implements OnInit {
  state$ = this.pageStateService.state$;
  breadcrumb?: PoBreadcrumb;

  constructor(private pageStateService: ResumeForRecruiterPageStateService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe({
      next: async (params) => {
        const openingId = params.openingId;
        const applicantId = params.applicantId;
        assert(openingId && applicantId);
        this.breadcrumb = {
          items: [
            {
              label: 'Opening management',
              link: '/opening/management'
            },
            {
              label: 'Applied applicants',
              link: `/opening/management/${openingId}/applied/applicants`
            },
            {
              label: 'Resume'
            }
          ]
        };
        await this.pageStateService.init(applicantId);
      }
    });
  }

}
