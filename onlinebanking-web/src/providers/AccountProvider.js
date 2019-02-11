import React from 'react';

import { connect } from 'react-redux';
import { fetchCurrentUser } from '../actions/users';

export const AccountContext = React.createContext();

class AccountProvider extends React.Component {

    componentWillMount() {
        this.props.fetchCurrentUser();
    }

    render() {
        return (
            <AccountContext.Provider value={{
                currentUser: this.props.userState.currentUser,
                userLoading: this.props.userState.userLoading
            }}>
                {
                    this.props.children
                }
            </AccountContext.Provider>
        );
    }
}

export default connect(
    state => ({
        userState: state.users
    }),
    dispatch => ({
        fetchCurrentUser: () => dispatch(fetchCurrentUser())
    })
)(AccountProvider);
