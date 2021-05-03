import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { State } from '../../utils/state';
import { Applicant } from './model/applicant.model';

@Injectable()
export class ResumeForRecruiterPageStateService extends State<Applicant> {
  constructor(private client: HttpClient) {
    super();
  }

  private getResume(applicantId: string): Promise<Applicant> {
    return this.client
      .get<Applicant>(`api/opening/management/applicant/${applicantId}`)
      .toPromise();
  }

  async init(applicantId: string): Promise<void> {
    const resume = await this.getResume(applicantId);
    this.forceSetState(resume);
  }
}
