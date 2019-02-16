import {
    FETCH_DEPOSIT_TYPES_REQUEST,
    FETCH_DEPOSIT_TYPES_SUCCESS,
    FETCH_DEPOSIT_TYPES_FAILURE
} from '../actions/deposits';

const defaultState = {
    depositTypes: [],
    deposits: [],
    error: null,
    loading: false
}

export default (state = defaultState, action) => {
    switch (action.type) {
        case FETCH_DEPOSIT_TYPES_REQUEST:
            return {
                ...state,
                loading: true
            };
        case FETCH_DEPOSIT_TYPES_SUCCESS:
            return {
                ...state,
                loading: false,
                depositTypes: action.depositTypes
            };
        case FETCH_DEPOSIT_TYPES_FAILURE:
            return {
                ...state,
                loading: false,
                error: action.error
            };

        default:
            return { ...state };
    }
}