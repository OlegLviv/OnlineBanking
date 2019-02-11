import React from 'react';
import BaseContainer from '../BaseContainer';
import ChangeCreditLimitForm from '../../components/ChangeCreditLimitForm/ChangeCreditLimitForm';

import { connect } from 'react-redux';
import { changeCreditLimit } from '../../actions/crediCards';
import { Spin } from 'antd';

class CreditCardChangeCreditLimitContainer extends BaseContainer {

    onSendChangeCreditLimit = body => {
        this.props.changeCreditLimit({
            newLimit: body.newLimit,
            id: this.props.match.params.id
        })
            .then(() => this.setState({ successMessage: "Credit limit success changed" }))
            .catch(er => {
                if (er && er.status && er.status === 400)
                    this.setState({ errorMessage: er.data });
            });
    }

    render() {
        const { loading } = this.props.creditCardState;

        return (
            <Spin spinning={loading}>
                <ChangeCreditLimitForm onSendChangeCreditLimit={this.onSendChangeCreditLimit} />
            </Spin>
        );
    }
}

export default connect(
    state => ({
        creditCardState: state.creditCards
    }),
    dispatch => ({
        changeCreditLimit: body => dispatch(changeCreditLimit(body))
    })
)(CreditCardChangeCreditLimitContainer);