import { apiGet } from '../api/api';

export default {
    getCreditCards() {
        return apiGet('/api/creditCards');
    }
}