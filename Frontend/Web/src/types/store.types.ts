import {Signal} from '@angular/core';

interface Store<TState, TStoreAction> {
  state: Signal<TState>;
  dispatch(action: TStoreAction): void;
}


interface StoreAction<TAction, TPayload> {
  type: TAction,
  payload: TPayload
}

type StoreReducer<TState, TPayload> = (state: TState, action: TPayload) => TState;

export type {Store, StoreAction, StoreReducer}
