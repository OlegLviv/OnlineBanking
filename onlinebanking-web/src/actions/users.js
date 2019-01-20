import userService from '../services/userService';
import { handleError } from './handleError';

export const SEND_USER_TWO_FA_REQUEST = 'SEND_USER_TWO_FA_REQUEST';
export const SEND_USER_TWO_FA_SUCCESS = 'SEND_USER_TWO_FA_SUCCESS';
export const SEND_USER_TWO_FA_FAILURE = 'SEND_USER_TWO_FA_FAILURE';

const sendUserTwoFaRequest = () => ({
    type: SEND_USER_TWO_FA_REQUEST
});

const sendUserTwoFaSuccess = userIdHolder => ({
    type: SEND_USER_TWO_FA_SUCCESS,
    userIdHolder
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