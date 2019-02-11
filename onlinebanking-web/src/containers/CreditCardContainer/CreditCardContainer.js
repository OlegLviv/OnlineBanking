import React from 'react';
import BaseContainer from '../BaseContainer';
import CreditCardType from '../../components/CreditCardType/CreditCardType';
import './CreditCardContainer.css';

import { connect } from 'react-redux';
import { fetchCreditCard } from '../../actions/crediCards';
import { Card, Icon, Spin, Menu, Dropdown } from 'antd';
import { dateToExpireCard } from '../../utils/timeUtils';
import { Link } from 'react-router-dom';
import { normalizeCardNumber } from '../../utils/creditCardUtil';

const { Meta } = Card;


const getIdFromUrl = ({ match }) => match.params.id;
const getRoleFromUrl = ({ match }) => match.params.role;

class CreditCardContainer extends BaseContainer {

    componentWillMount() {
        this.props.fetchCreditCard(getIdFromUrl(this.props));
    }

    renderMenu = () => (
        <Menu>
            <Menu.Item>
                <Link to={`/cab/${getRoleFromUrl(this.props)}/credit-card/${getIdFromUrl(this.props)}/change-pin`}>Change pin</Link>
            </Menu.Item>
            <Menu.Item>
                <Link to={`/cab/${getRoleFromUrl(this.props)}/credit-card/${getIdFromUrl(this.props)}/change-c-limit`}>Change credit limit</Link>
            </Menu.Item>
            <Menu.Item>
                <Link to={`/cab/${getRoleFromUrl(this.props)}/credit-card/${getIdFromUrl(this.props)}/lock`}>Change credit limit</Link>
            </Menu.Item>
        </Menu>
    );

    render() {
        const { creditCard, loading } = this.props.creditCardState;

        return (
            <div className="credit-card-box">
                {
                    loading ? <Spin /> :
                        <Card className="credit-card" title="Credit Card"
                            actions={
                                [
                                    <Dropdown overlay={this.renderMenu}>
                                        <Icon type="setting" />
                                    </Dropdown>,
                                    <Icon type="edit" />,
                                    <Icon type="ellipsis" />
                                ]
                            }>
                            <Meta
                                title={creditCard && normalizeCardNumber(creditCard.cardNumber)}
                            />
                            <Meta
                                description={creditCard && dateToExpireCard(creditCard.expired)}
                            />
                            <div className="icon-wrap">
                                <CreditCardType creditCard={creditCard} />
                            </div>
                            <div className="inline pad pad-top">
                                <Meta title="Balance:" />
                                <Meta className="pad-l" description={creditCard && `${creditCard.balance + creditCard.creditLimit - creditCard.credit} ₴`} />
                            </div>
                            <div className="inline pad">
                                <Meta title="Own funds:" />
                                <Meta className="pad-l" description={creditCard && creditCard.balance + ' ₴'} />
                            </div>
                            <div className="inline pad">
                                <Meta title="Credit limit:" />
                                <Meta className="pad-l" description={creditCard && creditCard.creditLimit + ' ₴'} />
                            </div>
                        </Card>
                }
            </div>
        );
    }
}

export default connect(
    state => ({
        creditCardState: state.creditCards
    }),
    dispatch => ({
        fetchCreditCard: id => dispatch(fetchCreditCard(id))
    })
)(CreditCardContainer);