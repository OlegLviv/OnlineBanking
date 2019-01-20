import tokenService from '../services/tokenService';
import { handleError } from './handleError';

export const FETCH_TOKE_REQUEST = 'FETCH_TOKE_REQUEST';
export const FETCH_TOKE_FAILURE = 'FETCH_TOKE_FAILURE';
export const FETCH_TOKE_SUCCESS = 'FETCH_TOKE_SUCCESS';

const fetchTokenRequest = () => ({
    type: FETCH_TOKE_REQUEST
});

const fetchTokenSuccess = token => ({
    type: FETCH_TOKE_SUCCESS,
    token
});

const fetchTokenFailure = error => ({
    type: FETCH_TOKE_FAILURE,
    error
});

export const fetchToken = (userId, code) => async dispatch => {
    dispatch(fetchTokenRequest());

    return tokenService.fetchToken(userId, code)
        .then(
            resp => dispatch(fetchTokenSuccess(resp.data)),
            error => handleError(dispatch, error, fetchTokenFailure)
        );
};