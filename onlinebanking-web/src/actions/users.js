import userService from '../services/userService';
import { handleError } from './handleError';

export const SEND_USER_TWO_FA_REQUEST = 'SEND_USER_TWO_FA_REQUEST';
export const SEND_USER_TWO_FA_SUCCESS = 'SEND_USER_TWO_FA_SUCCESS';
export const SEND_USER_TWO_FA_FAILURE = 'SEND_USER_TWO_FA_FAILURE';

export const FETCH_CURRENT_USER_REQUEST = 'FETCH_CURRENT_USER_REQUEST';
export const FETCH_CURRENT_USER_SUCCESS = 'FETCH_CURRENT_USER_SUCCESS';
export const FETCH_CURRENT_USER_FAILURE = 'FETCH_CURRENT_USER_FAILURE';

const sendUserTwoFaRequest = () => ({
    type: SEND_USER_TWO_FA_REQUEST
});

const sendUserTwoFaSuccess = twoFactorInfo => ({
    type: SEND_USER_TWO_FA_SUCCESS,
    twoFactorInfo
});

const sendUserTwoFailure = error => ({
    type: SEND_USER_TWO_FA_FAILURE,
    error
});

export const sendUserTwoFa = body => async dispatch => {
    dispatch(sendUserTwoFaRequest());

    return userService
        .sendUser2fa(body)
        .then(
            resp => dispatch(sendUserTwoFaSuccess(resp.data)),
            error => handleError(dispatch, error, sendUserTwoFailure)
        );
};

const fetchCurrentUserRequest = () => ({
    type: FETCH_CURRENT_USER_REQUEST
});

const fetchCurrentUserSuccess = currentUser => ({
    type: FETCH_CURRENT_USER_SUCCESS,
    currentUser
});

const fetchCurrentUserFailure = error => ({
    type: FETCH_CURRENT_USER_FAILURE,
    error
});

export const fetchCurrentUser = () => async dispatch => {
    dispatch(fetchCurrentUserRequest());

    return userService
        .getCurrentUser()
        .then(
            resp => dispatch(fetchCurrentUserSuccess(resp.data)),
            error => handleError(dispatch, error, fetchCurrentUserFailure)
        );
}

