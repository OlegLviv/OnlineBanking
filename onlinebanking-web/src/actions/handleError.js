export const handleError = (dispatch, error, failFunc) => {
    console.error('error from handle', error);

    if (error && error.status >= 400 && error.status < 500) {
        dispatch(failFunc(error));
        return Promise.reject(error);
    }

    dispatch(failFunc(error));
    return Promise.reject(error);
};