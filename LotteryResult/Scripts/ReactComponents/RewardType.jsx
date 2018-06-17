class RewardType extends React.Component {

    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div class="panel panel-default">
                <div class="panel-heading">ข้อมูลประเภทรางวัล</div>
                <div class="panel-body">
                    <p>ประเภทรางวัล: {this.props.rewardType.name}</p>
                    <p>จำนวนรางวัล: {this.props.rewardType.instance}</p>
                    <p>รูปแบบข้อมูล: {this.props.rewardType.format}</p>
                    <p>จำนวนเงินรางวัล: {this.props.rewardType.reward_amount}</p>
                </div>
            </div>
        );
    }
}