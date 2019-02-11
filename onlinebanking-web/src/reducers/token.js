import {
    FETCH_TOKE_REQUEST,
    FETCH_TOKE_FAILURE,
    FETCH_TOKE_SUCCESS
} from '../actions/token';

const defaultState = {
    accessToken: null,
    loading: false,
    error: null
}

export default (state = defaultState, action) => {
    switch (action.type) {
        case FETCH_TOKE_REQUEST:
            return {
                ...state,
                loading: true,
            };
        case FETCH_TOKE_SUCCESS:
            return {
                ...state,
                accessToken: action.accessToken,
                loading: false
            };
        case FETCH_TOKE_FAILURE:
            return {
                ...state,
                error: action.error,
                loading: false
            };
        default:
            return { ...state };
    }
}