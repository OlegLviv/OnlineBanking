import depositsService from '../services/depositsService';
import { handleError } from './handleError';

export const FETCH_DEPOSIT_TYPES_REQUEST = 'FETCH_DEPOSIT_TYPES_REQUEST';
export const FETCH_DEPOSIT_TYPES_SUCCESS = 'FETCH_DEPOSIT_TYPES_SUCCESS';
export const FETCH_DEPOSIT_TYPES_FAILURE = 'FETCH_DEPOSIT_TYPES_FAILURE';

export const FETCH_DEPOSITS_REQUEST = 'FETCH_DEPOSITS_REQUEST';
export const FETCH_DEPOSITS_SUCCESS = 'FETCH_DEPOSITS_SUCCESS';
export const FETCH_DEPOSITS_FAILURE = 'FETCH_DEPOSITS_FAILURE';

export const CREATE_DEPOSIT_REQUEST = 'CREATE_DEPOSIT_REQUEST';
export const CREATE_DEPOSIT_SUCCESS = 'CREATE_DEPOSIT_SUCCESS';
export const CREATE_DEPOSIT_FAILURE = 'CREATE_DEPOSIT_FAILURE';

const fetchDepositTypesRequest = () => ({
    type: FETCH_DEPOSIT_TYPES_REQUEST
});

const fetchDepositTypesSuccess = depositTypes => ({
    type: FETCH_DEPOSIT_TYPES_SUCCESS,
    depositTypes
});

const fetchDepositTypesFailure = error => ({
    type: FETCH_DEPOSIT_TYPES_FAILURE,
    error
});

export const fetchDepositTypes = currency => async dispatch => {
    dispatch(fetchDepositTypesRequest());

    return depositsService
        .getTepositTypes(currency)
        .then(
            resp => dispatch(fetchDepositTypesSuccess(resp.data)),
            error => handleError(dispatch, error, fetchDepositTypesFailure)
        );
};

const createDepositRequest = () => ({
    type: CREATE_DEPOSIT_REQUEST
});

const createDepositSuccess = deposit => ({
    type: CREATE_DEPOSIT_SUCCESS,
    deposit
});

const createDepositFailure = error => ({
    type: CREATE_DEPOSIT_FAILURE,
    error
});

export const createDeposit = body => async dispatch => {
    dispatch(createDepositRequest());

    return depositsService
        .createDeposit(body)
        .then(
            resp => dispatch(createDepositSuccess(resp.data)),
            error => handleError(dispatch, error, createDepositFailure)
        );
};

const fetchDepositsRequest = () => ({
    type: FETCH_DEPOSITS_REQUEST
});

const fethcDepositsSuccess = deposits => ({
    type: FETCH_DEPOSITS_SUCCESS,
    deposits
});

const fetchDepositsFailure = error => ({
    type: FETCH_DEPOSITS_FAILURE,
    error
});

export const fetchDeposits = () => async dispatch => {
    dispatch(fetchDepositsRequest());

    return depositsService
        .getDeposits()
        .then(
            resp => dispatch(fethcDepositsSuccess(resp.data)),
            err => handleError(dispatch, err, fetchDepositsFailure)
        );
};