import {
    FETCH_DEPOSIT_TYPES_REQUEST,
    FETCH_DEPOSIT_TYPES_SUCCESS,
    FETCH_DEPOSIT_TYPES_FAILURE,

    CREATE_DEPOSIT_REQUEST,
    CREATE_DEPOSIT_SUCCESS,
    CREATE_DEPOSIT_FAILURE
} from '../actions/deposits';

const defaultState = {
    depositTypes: [],
    deposits: [],
    deposit: null,
    error: null,
    loading: false
}

export default (state = defaultState, action) => {
    switch (action.type) {
        case FETCH_DEPOSIT_TYPES_REQUEST:
        case CREATE_DEPOSIT_REQUEST:
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
        case CREATE_DEPOSIT_SUCCESS:
            return {
                ...state,
                loading: false,
                deposit: action.deposit
            };
        case FETCH_DEPOSIT_TYPES_FAILURE:
        case CREATE_DEPOSIT_FAILURE:
            return {
                ...state,
                loading: false,
                error: action.error
            };

        default:
            return { ...state };
    }
}