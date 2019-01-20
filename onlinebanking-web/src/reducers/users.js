import {
    SEND_USER_TWO_FA_REQUEST,
    SEND_USER_TWO_FA_SUCCESS,
    SEND_USER_TWO_FA_FAILURE
} from '../actions/users';

const defaultState = {
    currentUser: null,
    user: null,
    loading: false,
    editLoading: false,
    error: null,
    userIdHolder: null
}

export default (state = defaultState, action) => {
    switch (action.type) {
        case SEND_USER_TWO_FA_REQUEST:
            return {
                ...state,
                loading: true
            };
        case SEND_USER_TWO_FA_SUCCESS:
            return {
                ...state,
                userIdHolder: action.userIdHolder,
                loading: false
            };
        case SEND_USER_TWO_FA_FAILURE:
            return {
                ...state,
                loading: false,
                error: action.error
            };

        default:
            return { ...state };
    }
}