import { Draft, produce } from 'immer';
import { BehaviorSubject, Observable } from 'rxjs';
import { first } from 'rxjs/operators';

export class State<T> {
  private stateSubject: BehaviorSubject<T | undefined>;
  readonly state$: Observable<T | undefined>;

  get snapshot(): T | undefined {
    return this.stateSubject.value;
  }

  constructor(initialState?: T) {
    this.stateSubject = new BehaviorSubject<T | undefined>(initialState == null ? undefined : {...initialState});
    this.state$ = this.stateSubject.asObservable();
  }

  protected setState(callBack: (draft: Draft<T>) => void): T | undefined {
    const newState = produce(this.snapshot, callBack);
    this.stateSubject.next(newState);
    return newState;
  }

  protected forceSetState(state: T | undefined ): void {
    this.stateSubject.next(state);
  }

  getFirst(): Observable<T | undefined > {
    return this.state$.pipe(first());
  }
}
