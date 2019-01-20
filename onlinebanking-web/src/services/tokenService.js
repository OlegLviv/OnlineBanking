import { publicApi } from '../api/api';

export default {
    fetchToken(userId, code) {
        return publicApi
            .get(`/api/token/${userId}/${code}`);
    }
}