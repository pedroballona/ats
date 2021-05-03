import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { assert } from '../../utils/assert';
import { State } from '../../utils/state';
import { SimpleApplicant } from './models/simple-applicant.model';

export interface OpeningAppliedApplicantsListingPageState {
  readonly openingId?: string;
  readonly applicants: Array<SimpleApplicant>;
  readonly filter?: string;
}

const initialState: OpeningAppliedApplicantsListingPageState = {
  applicants: [],
};

@Injectable()
export class OpeningAppliedApplicantsListingPageStateService extends State<OpeningAppliedApplicantsListingPageState> {
  constructor(private client: HttpClient) {
    super(initialState);
  }

  private getAppliedApplicants(
    openingId: string,
    filter?: string
  ): Promise<SimpleApplicant[]> {
    const params = filter ? { filter } : undefined;
    return this.client
      .get<SimpleApplicant[]>(
        `api/opening/management/${openingId}/applied/applicants`,
        { params }
      )
      .toPromise();
  }

  async init(openingId: string): Promise<void> {
    const result = await this.getAppliedApplicants(openingId);
    this.setState(draft => {
      draft.openingId = openingId;
      draft.applicants = result;
    });
  }

  async setFilter(filter: string | undefined): Promise<void> {
    const openingId = this.snapshot?.openingId;
    assert(openingId);
    const result = await this.getAppliedApplicants(openingId, filter);
    this.setState(draft => {
      draft.openingId = openingId;
      draft.applicants = result;
      draft.filter = filter;
    });
  }
}
