import { apiGet } from '../api/api';

export default {
    getTepositTypes(currency) {
        return apiGet(`/api/deposits/depositTypes/${currency}`);
    }
}