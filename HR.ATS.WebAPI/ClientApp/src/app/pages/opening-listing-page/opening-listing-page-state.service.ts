import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { State } from '../../utils/state';
import { Opening } from '../opening-management-page/models/opening.model';

export interface OpeningListingPageState {
  readonly items: Array<Opening>;
  readonly filter?: string;
}

const initialState: OpeningListingPageState = {
  items: [],
};

@Injectable()
export class OpeningListingPageStateService extends State<OpeningListingPageState> {

  constructor(private client: HttpClient) {
    super(initialState);
  }

  private getAllOpenings(filter?: string): Promise<Opening[]> {
    const params = filter ? { filter } : undefined;
    return this.client
      .get<Opening[]>('/api/opening/listing', { params })
      .toPromise();
  }

  private applyToOpening(id: string): Promise<Opening[]> {
    return this.client
      .post<Opening[]>(`/api/opening/listing/${id}/apply`, null)
      .toPromise();
  }

  async init(): Promise<void> {
    const openings = await this.getAllOpenings();
    this.setState((draft) => {
      draft.items = openings;
    });
  }

  async setFilter(filter: string | undefined): Promise<void> {
    filter = filter?.trim();
    if (filter == null) {
      return;
    }
    const openings = await this.getAllOpenings(filter);
    this.setState((draft) => {
      draft.items = openings;
      draft.filter = filter;
    });
  }

  async apply(openingId: string): Promise<void> {
    await this.applyToOpening(openingId);
  }
}
