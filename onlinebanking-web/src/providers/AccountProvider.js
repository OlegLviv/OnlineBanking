import React from 'react';

import { connect } from 'react-redux';
import { fetchCurrentUser } from '../actions/users';
import { Spin } from 'antd';

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
                <Spin spinning={this.props.userState.userLoading}>
                    {
                        this.props.children
                    }
                </Spin>
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
