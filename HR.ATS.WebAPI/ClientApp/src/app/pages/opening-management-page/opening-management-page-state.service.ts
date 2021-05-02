import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { State } from '../../utils/state';
import { Opening } from './models/opening.model';

export interface OpeningManagementPageState {
  readonly items: Array<Opening>;
  readonly filter?: string;
}

const initialState: OpeningManagementPageState = {
  items: [],
};

@Injectable()
export class OpeningManagementPageStateService extends State<OpeningManagementPageState> {
  constructor(private client: HttpClient) {
    super(initialState);
  }

  private getAllOpenings(filter?: string): Promise<Opening[]> {
    const params = filter ? { filter } : undefined;
    return this.client
      .get<Opening[]>('/api/opening/management', { params })
      .toPromise();
  }

  private saveOpening(opening: Opening): Promise<Opening> {
    return this.client
      .post<Opening>('/api/opening/management', opening)
      .toPromise();
  }

  private async deleteOpening(id: string): Promise<void> {
    await this.client
      .delete(`/api/opening/management/${id}`)
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

  async save(opening: Opening): Promise<void> {
    await this.saveOpening(opening);
    const openings = await this.getAllOpenings(this.snapshot?.filter);
    this.setState((draft) => {
      draft.items = openings;
    });
  }

  async delete(id: string): Promise<void> {
    await this.deleteOpening(id);
    const openings = await this.getAllOpenings(this.snapshot?.filter);
    this.setState((draft) => {
      draft.items = openings;
    });
  }
}
