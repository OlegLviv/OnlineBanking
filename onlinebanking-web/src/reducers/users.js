import {
    SEND_USER_TWO_FA_REQUEST,
    SEND_USER_TWO_FA_SUCCESS,
    SEND_USER_TWO_FA_FAILURE,

    FETCH_CURRENT_USER_REQUEST,
    FETCH_CURRENT_USER_SUCCESS,
    FETCH_CURRENT_USER_FAILURE
} from '../actions/users';

const defaultState = {
    currentUser: null,
    user: null,
    loading: false,
    userLoading: false,
    error: null,
    twoFactorInfo: null
}

export default (state = defaultState, action) => {
    switch (action.type) {
        case SEND_USER_TWO_FA_REQUEST:
            return {
                ...state,
                loading: true
            };
        case FETCH_CURRENT_USER_REQUEST:
            return {
                ...state,
                userLoading: true
            };
        case SEND_USER_TWO_FA_SUCCESS:
            return {
                ...state,
                twoFactorInfo: action.twoFactorInfo,
                loading: false
            };
        case FETCH_CURRENT_USER_SUCCESS:
            return {
                ...state,
                currentUser: action.currentUser,
                userLoading: false
            };
        case SEND_USER_TWO_FA_FAILURE:
            return {
                ...state,
                loading: false,
                error: action.error
            };
        case FETCH_CURRENT_USER_FAILURE:
            return {
                ...state,
                userLoading: false,
                error: action.error
            };

        default:
            return { ...state };
    }
}