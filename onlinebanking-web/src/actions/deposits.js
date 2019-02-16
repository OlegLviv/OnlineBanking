import depositsService from '../services/depositsService';
import { handleError } from './handleError';

export const FETCH_DEPOSIT_TYPES_REQUEST = 'FETCH_DEPOSIT_TYPES_REQUEST';
export const FETCH_DEPOSIT_TYPES_SUCCESS = 'FETCH_DEPOSIT_TYPES_SUCCESS';
export const FETCH_DEPOSIT_TYPES_FAILURE = 'FETCH_DEPOSIT_TYPES_FAILURE';

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