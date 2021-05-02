import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { State } from '../../utils/state';
import { Resume } from './model/resume.model';

@Injectable()
export class ResumePageStateService extends State<Resume> {
  constructor(private client: HttpClient) {
    super();
  }

  async init(): Promise<void> {
    const result = await this.getResumeForLoggedPerson();
    this.forceSetState(result);
  }

  private async getResumeForLoggedPerson(): Promise<Resume | undefined> {
    try {
      const resume = await this.client
        .get<Resume>('/api/applicants/logged/resume')
        .toPromise();
      return resume;
    } catch (err) {
      if (err instanceof HttpErrorResponse && err.status === 404) {
        return undefined;
      }
      throw err;
    }
  }

  private async updateResume(resume: Resume): Promise<Resume | undefined> {
    return await this.client
      .put<Resume>('/api/applicants/logged/resume', resume)
      .toPromise();
  }

  async save(resume: Resume): Promise<void> {
    const savedResume = await this.updateResume(resume);
    this.forceSetState(savedResume);
  }
}
