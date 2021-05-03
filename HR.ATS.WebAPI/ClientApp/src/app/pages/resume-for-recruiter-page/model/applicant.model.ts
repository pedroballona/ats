import { Resume } from '../../resume-page/model/resume.model';

export interface Applicant {
  readonly name: string;
  readonly resume: Resume;
}
