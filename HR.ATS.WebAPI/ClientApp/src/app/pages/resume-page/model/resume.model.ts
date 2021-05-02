export interface Resume {
  readonly introduction: string;
  readonly experiences: Experience[];
}

export interface Experience {
  readonly company: string;
  readonly description: string;
  readonly periodStartDate: string;
  readonly periodEndDate?: string;
}
